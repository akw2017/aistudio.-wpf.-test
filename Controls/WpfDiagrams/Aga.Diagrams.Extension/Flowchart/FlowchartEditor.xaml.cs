using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Aga.Diagrams;
using Aga.Diagrams.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace Aga.Diagrams.Extension.Flowchart
{
	public partial class FlowchartEditor : UserControl
	{
        public static readonly DependencyProperty IsThemeChangedProperty =
           DependencyProperty.Register("IsThemeChanged",
                                      typeof(bool),
                                      typeof(FlowchartEditor),
                                      new FrameworkPropertyMetadata(false));

        public bool IsThemeChanged
        {
            get { return (bool)GetValue(IsThemeChangedProperty); }
            set { SetValue(IsThemeChangedProperty, value); }
        }

        private ItemsControlDragHelper _dragHelper;

		public FlowchartEditor()
		{
			InitializeComponent();

			var model = CreateModel();
			_view.Controller = new Controller(this, _view, model);
			_view.DragDropTool = new DragDropTool(_view, model);
			_view.DragTool = new CustomMoveResizeTool(_view, model) 
			{ 
				MoveGridCell = _view.GridCellSize
			};
			_view.LinkTool = new CustomLinkTool(_view);
			_view.Selection.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Selection_PropertyChanged);
			_dragHelper = new ItemsControlDragHelper(_toolbox, this);

			FillToolbox();
		}

		private void FillToolbox()
		{
            _toolbox.Items.Add(CreateToolbox(NodeKinds.Start));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.End));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.Action));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.Condition));
        }

        private FrameworkElement CreateToolbox(NodeKinds nk)
        {
            var node = new FlowNode(nk);
            node.Label = nk.ToString();
            var ui = Controller.CreateContent(this, node);
            ui.Width = 60;
            ui.Height = 30;
            ui.Margin = new Thickness(5);
            ui.Tag = nk;
            return ui;
        }

		private FlowchartModel CreateModel()
		{
			var model = new FlowchartModel();
			
			var start = new FlowNode(NodeKinds.Start);
			start.Row = 0;
			start.Column = 1;
			start.Label = "Start";

			var ac0 = new FlowNode(NodeKinds.Action);
			ac0.Row = 1;
			ac0.Column = 1;
			ac0.Label = "i = 0";

			var cond = new FlowNode(NodeKinds.Condition);
			cond.Row = 2;
			cond.Column = 1;
			cond.Label = "i < n";

			var ac1 = new FlowNode(NodeKinds.Action);
			ac1.Row = 3;
			ac1.Column = 1;
			ac1.Label = "do something";

			var ac2 = new FlowNode(NodeKinds.Action);
			ac2.Row = 4;
			ac2.Column = 1;
			ac2.Label = "i++";

			var end = new FlowNode(NodeKinds.End);
			end.Row = 3;
			end.Column = 2;
			end.Label = "End";

			model.Nodes.Add(start);
			model.Nodes.Add(cond);
			model.Nodes.Add(ac0);
			model.Nodes.Add(ac1);
			model.Nodes.Add(ac2);
			model.Nodes.Add(end);

			model.Links.Add(new Link(start, PortKinds.Bottom, ac0, PortKinds.Top));
			model.Links.Add(new Link(ac0, PortKinds.Bottom, cond, PortKinds.Top));
			
			model.Links.Add(new Link(cond, PortKinds.Bottom, ac1, PortKinds.Top) { Label = "True" });
			model.Links.Add(new Link(cond, PortKinds.Right, end, PortKinds.Top) { Label = "False" });

			model.Links.Add(new Link(ac1, PortKinds.Bottom, ac2, PortKinds.Top));
			model.Links.Add(new Link(ac2, PortKinds.Bottom, cond, PortKinds.Top));

			return model;
		}

		void Selection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var p = _view.Selection.Primary;
			_propertiesView.SelectedObject = p != null ? p.ModelElement : null;
		}

    }
}

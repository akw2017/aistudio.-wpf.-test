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
using System.Reflection;

namespace Aga.Diagrams.Extension.Flowchart
{
	public partial class OAFlowEditor : UserControl
	{
        public static readonly DependencyProperty IsThemeChangedProperty =
           DependencyProperty.Register("IsThemeChanged",
                                      typeof(bool),
                                      typeof(OAFlowEditor),
                                      new FrameworkPropertyMetadata(false));

        public bool IsThemeChanged
        {
            get { return (bool)GetValue(IsThemeChangedProperty); }
            set { SetValue(IsThemeChangedProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty =
          DependencyProperty.Register("Mode",
                                     typeof(string),
                                     typeof(OAFlowEditor),
                                     new FrameworkPropertyMetadata("Edit", ModeChanged));

        public string Mode
        {
            get { return (string)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        private static void ModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as OAFlowEditor;
            var mode = e.NewValue as string;
            if (mode != "Edit")
            {
                view._toolcol.Visibility = Visibility.Collapsed;
                view._view.Mode = mode;
            }
            else
            {
                view._toolcol.Visibility = Visibility.Visible;
                view._view.Mode = mode; 
            }
        }

        //一点要绑定不为空的FlowchartModel才能用，即便为空的也要new一个再来绑定
        public static readonly DependencyProperty FlowchartModelProperty =
          DependencyProperty.Register("FlowchartModel",
                                     typeof(FlowchartModel),
                                     typeof(OAFlowEditor),
                                     new FrameworkPropertyMetadata(null, FlowchartModelChanged));

        public FlowchartModel FlowchartModel
        {
            get { return (FlowchartModel)GetValue(FlowchartModelProperty); }
            set { SetValue(FlowchartModelProperty, value); }
        }
        private static void FlowchartModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as OAFlowEditor;
            var model = e.NewValue as FlowchartModel;
            if (model != null)
            {
                view.CreateFlowchartModel(model);
            }
        }

        #region SelectedObject

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(object), typeof(OAFlowEditor), new UIPropertyMetadata(null));
        public object SelectedObject
        {
            get
            {
                return (object)GetValue(SelectedObjectProperty);
            }
            set
            {
                SetValue(SelectedObjectProperty, value);
            }
        }

        #endregion //SelectedObject

        private ItemsControlDragHelper _dragHelper;

		public OAFlowEditor()
		{
			InitializeComponent();	
			FillToolbox();            
        }

        private void CreateFlowchartModel(FlowchartModel model)
        {
            _view.Controller = new Controller(this, _view, model);
            _view.DragDropTool = new DragDropTool(_view, model);
            _view.DragTool = new CustomMoveResizeTool(_view, model)
            {
                MoveGridCell = _view.GridCellSize
            };
            _view.LinkTool = new CustomLinkTool(_view);
            _view.Selection.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Selection_PropertyChanged);
          
        }

        private void FillToolbox()
		{
            _toolbox.Items.Add(CreateToolbox(NodeKinds.Start));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.End));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.Middle));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.Decide));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.COBegin));
            _toolbox.Items.Add(CreateToolbox(NodeKinds.COEnd));
            _dragHelper = new ItemsControlDragHelper(_toolbox, this);
        }       

        private FrameworkElement CreateToolbox(NodeKinds nk)
        {
            var node = new FlowNode(nk);
            node.Label = nk.GetDescription();
            var ui = Controller.CreateContent(this, node);
            ui.Width = 60;
            ui.Height = 30;
            ui.Margin = new Thickness(5);
            ui.Tag = nk;
            return ui;
        }
	
		void Selection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var p = _view.Selection.Primary;
		    SelectedObject = p != null ? p.ModelElement : null;
		}

       
    }


}

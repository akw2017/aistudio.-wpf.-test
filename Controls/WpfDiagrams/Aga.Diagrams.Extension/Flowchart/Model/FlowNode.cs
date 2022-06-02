using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Aga.Diagrams.Extension.Flowchart
{
    public class FlowNode: INotifyPropertyChanged
	{

        [Browsable(false)]
        public NodeKinds Kind { get; private set; }

        private int _column;
		public int Column
		{
			get { return _column; }
			set 
			{ 
				_column = value;
				OnPropertyChanged("Column");
			}
		}

		private int _row;
		public int Row
		{
			get { return _row; }
			set 
			{ 
				_row = value;
				OnPropertyChanged("Row");
			}
		}

		private string _label;
		public string Label
		{
			get { return _label; }
			set
			{
				_label = value;
				OnPropertyChanged("Label");
			}
		}

        public FlowNode(NodeKinds kind)
		{
			Kind = kind;
		}

		public IEnumerable<PortKinds> GetPorts()
		{
			switch(Kind)
			{
				case NodeKinds.Start:
					yield return PortKinds.Bottom;
					break;
				case NodeKinds.End:
					yield return PortKinds.Top;
					break;
				case NodeKinds.Action:
					yield return PortKinds.Top;
					yield return PortKinds.Bottom;
					break;
				case NodeKinds.Condition:
					yield return PortKinds.Top;
					yield return PortKinds.Bottom;
					yield return PortKinds.Left;
					yield return PortKinds.Right;
					break;
                default:
                    yield return PortKinds.Top;
                    yield return PortKinds.Bottom;
                    yield return PortKinds.Left;
                    yield return PortKinds.Right;
                    break;
            }
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

    public enum NodeKinds
    { 
        [Description("开始")]
        Start,
        [Description("结束")]
        End,
        [Description("动作节点")]
        Action,
        [Description("条件节点")]
        Condition,
        [Description("中间节点")]
        Middle,
        [Description("条件节点")]
        Decide,
        [Description("并行开始节点")]
        COBegin,
        [Description("并行结束节点")]
        COEnd,
        [Description("通用节点")]
        Normal 
    }
}

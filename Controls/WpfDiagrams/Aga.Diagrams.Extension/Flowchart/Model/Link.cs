using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Aga.Diagrams.Extension.Flowchart
{
    public class Link: INotifyPropertyChanged
	{

        [Browsable(false)]
		public FlowNode Source { get; private set; }
        [Browsable(false)]
		public PortKinds SourcePort { get; private set; }
		[Browsable(false)]
		public FlowNode Target { get; private set; }
        [Browsable(false)]
		public PortKinds TargetPort { get; private set; }

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


		public Link(FlowNode source, PortKinds sourcePort, FlowNode target, PortKinds targetPort)
		{
			Source = source;
			SourcePort = sourcePort;
			Target = target;
			TargetPort = targetPort;
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

    public enum PortKinds { Top, Bottom, Left, Right }
}

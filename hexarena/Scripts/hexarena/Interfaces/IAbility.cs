using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface  IAbility
	{

		public abstract List<ITarget> GetTargetList();
		abstract void Use(object sender, EventArgs e);

		public virtual void Initialize(EventHandler e) 
		{
			e += Use;
		}

	}
}

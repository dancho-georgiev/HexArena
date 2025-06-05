using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IAbility
	{
	List<ITarget> Targets { get; set; }

	void Use(object sender, EventArgs e);

	void AddTarget(ITarget target){
		Targets.Add(target);
	}

	void Connect(EventHandler handler)
	{
		handler += Use; // Only legal in newer versions with support
	}
	}
}

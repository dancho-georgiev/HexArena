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

	void Initialize(EventHandler handler)
	{
		handler += Use; // Only legal in newer versions with support
	}
	}
}

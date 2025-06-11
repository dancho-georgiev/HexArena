using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;

namespace Interfaces
{
	public interface IAbility
	{
		List<ITarget> Targets { get; set; }
		void Use();
		void AddTarget(ITarget target);
		void Connect(EventManager eventManager);
		void Disconnect(EventManager eventManager);
	}
}

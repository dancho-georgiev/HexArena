using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface ICharacter : ITargetable
	{
		int Health { get; protected set; }

		ITile Tile { get; protected set; }
	}
}

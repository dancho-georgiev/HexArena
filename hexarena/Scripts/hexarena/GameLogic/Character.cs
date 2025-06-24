using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using Utilities;
using System.Linq;

namespace GameLogic
{
	
	public abstract class Character : Targetable, ICharacter
	{
		private ITile tile;
		public int Health { get;  set; }
		public int Initiative {get; set;}
		public double StepEnergyCost { get;  set; }
		public List<IStatusEffect> StatusEffects {get; set;}
		public List<IActive> ActiveAbilities { get; set; }
		public List<IPassive> PassiveAbilities { get; set; }
		public ITile Tile { get{return tile;} 
		set{
				tile = value;
				tile.CharacterOnTile = this;
				if(ActiveAbilities == null || PassiveAbilities==null) return;
				foreach(IRangeRestrictedTarget globalTarget in
				 this.ActiveAbilities.Where(x=> x.Target is IRangeRestrictedTarget).Select(x=>x.Target)){
					globalTarget.Position = tile;
				}
				foreach(IRangeRestrictedTarget globalTarget in
				 this.PassiveAbilities.Where(x=> x.Target is IRangeRestrictedTarget).Select(x=>x.Target)){
					globalTarget.Position = tile;
				}
			}
		 }
		
		public Character(int health,double stepEnergyCost, int Initiative)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Initiative = Initiative;
			StatusEffects = new List<IStatusEffect>();
			Tile = new Tile(new Point(0,0), this);
		}
		
		protected abstract void InitializeActives();
		protected abstract void InitializePassives();
		
		//curently used for vulnerable
		//promenq podaden damage spored status efectite na Charactera
		//returns modified damage
		public virtual int ModifyDamageTaken(int damage) 
		{
			//GD.Print($"ModifyDamageTaken entered with {damage}");
			
			float totalPercent = 0f;

			foreach (IStatusEffect effect in StatusEffects)
			{
				if (effect is IModifyDamageTaken modifier)
				{
					totalPercent += modifier.GetBonusPercent(); //set in the modifier
				}
			}

			int modifiedDamage = (int)Math.Ceiling(damage * (1f + totalPercent));

			//GD.Print($"ModifyDamageTaken exited with {modifiedDamage}");
			return modifiedDamage;
		}
		public override void TakeDamage(int damage){
			int modifiedDamage = ModifyDamageTaken(damage);
			Health -= modifiedDamage;
		}

		public override void TakeStatusEffect(IStatusEffect statusEffect){
			StatusEffects.Add(statusEffect);
		}
		
		public void TeleportCharacter(ITile TargetPosition)
		{
			if(TargetPosition.IsAvailable())
			{
				this.Tile = TargetPosition;
			}
		}
	
	//Grisho:tva trqq da vleze samo na characterite koito she nqma da sa playable(na enemytata)
	// po nqkoe vreme go premesti
	// Dancho: Ne tova shte vleze na vseki 
	//Dancho: grisho e prav :klumnala roza:
		public virtual void MoveCharacter(ITile TargetPosition)
		{
			if(TargetPosition.IsAvailable())
			{
				List<ITile> pathTiles = Utility.FindShortestPath(this.Tile, TargetPosition);
				ITile lastTile = pathTiles[0]; // this helps clearing the characters which we create* along the path
				foreach(ITile i in pathTiles)
				{
					lastTile.CharacterOnTile = null;
					//lastTile.IsAvailable = true;
					this.Tile = i;
					i.CharacterOnTile = this;
					//i.IsAvailable = false;
					lastTile = i;
					//GD.Print($"{this.Tile.Position.x}, {this.Tile.Position.y}");
				}
				//GD.Print($"curent pos {this.Tile.Position.x}, {this.Tile.Position.y}");

			}
		}
		
		public virtual void MoveCharacter(List<ITile> path)
		{
			if(path.Last().IsAvailable())
			{
				List<ITile> pathTiles = path;
				ITile lastTile = pathTiles[0]; // this helps clearing the characters which we create* along the path
				foreach(ITile i in pathTiles)
				{
					lastTile.CharacterOnTile = null;
					//lastTile.IsAvailable = true;
					this.Tile = i;
					i.CharacterOnTile = this;
					//i.IsAvailable = false;
					lastTile = i;
					//GD.Print($"{this.Tile.Position.x}, {this.Tile.Position.y}");
				}
				//GD.Print($"curent pos {this.Tile.Position.x}, {this.Tile.Position.y}");
			}
		}
	}
}

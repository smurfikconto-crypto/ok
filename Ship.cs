// Statki + dekorator + composite + fabryka + prototype
using Projekt_ZTP;
using System.Collections.Generic;

namespace Projekt_ZTP
{
    public interface IShip
    {
        int GetLength();
        bool IsSunk();
        bool Hit(Position pos);
    }

    public class CompositeShip : IShip
    {
        private readonly List<IShip> _children = new();

        public void AddShip(IShip ship) => _children.Add(ship);
        public void RemoveShip(IShip ship) => _children.Remove(ship);

        public int GetLength()
        {
            int sum = 0;
            foreach (var s in _children) sum += s.GetLength();
            return sum;
        }

        public bool IsSunk()
        {
            foreach (var s in _children)
                if (!s.IsSunk()) return false;
            return true;
        }

        public bool Hit(Position pos)
        {
            bool hit = false;
            foreach (var s in _children)
                hit |= s.Hit(pos);
            return hit;
        }
    }

    public class ShipDecorator : IShip
    {
        protected readonly IShip _decorated;

        public ShipDecorator(IShip ship)
        {
            _decorated = ship;
        }

        public virtual int GetLength() => _decorated.GetLength();
        public virtual bool IsSunk() => _decorated.IsSunk();
        public virtual bool Hit(Position pos) => _decorated.Hit(pos);
    }

    public abstract class ShipPrototype : IShip
    {
        protected int length;
        protected int hitPoints;
        protected string type = "";

        public abstract IShip Clone();

        public int GetLength() => length;
        public bool IsSunk() => hitPoints <= 0;

        public bool Hit(Position pos)
        {
            if (hitPoints > 0)
            {
                hitPoints--;
                return true;
            }
            return false;
        }
    }

    public class CarrierPrototype : ShipPrototype
    {
        public CarrierPrototype()
        {
            length = 5;
            hitPoints = 5;
            type = "Carrier";
        }

        public override IShip Clone()
        {
            return new CarrierPrototype();
        }
    }

    public class BattleshipPrototype : ShipPrototype
    {
        public BattleshipPrototype()
        {
            length = 4;
            hitPoints = 4;
            type = "Battleship";
        }

        public override IShip Clone()
        {
            return new BattleshipPrototype();
        }
    }

    public abstract class AbstractShipFactory
    {
        public abstract IShip CreateCarrier();
        public abstract IShip CreateBattleship();
        public abstract IShip CreateDestroyer();
        public abstract IShip CreateSubmarine();
    }

    public class ConcreteStandardFactory : AbstractShipFactory
    {
        private readonly ShipPrototype _carrier = new CarrierPrototype();
        private readonly ShipPrototype _battleship = new BattleshipPrototype();
        // Dodać DestroyerPrototype i SubmarinePrototype

        public override IShip CreateCarrier() => _carrier.Clone();
        public override IShip CreateBattleship() => _battleship.Clone();
        public override IShip CreateDestroyer() => _battleship.Clone();
        public override IShip CreateSubmarine() => _battleship.Clone();
    }
}

using Stseniayeva.Domain.Entities;

namespace Stseniayeva.UI.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }

        /// <summary>
        /// Количество калорий
        /// </summary>
        public int SpeedMax
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Moto.SpeedMax);
            }
        }

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="moto">добавляемый объект</param>
        public virtual void AddToCart(Moto moto)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(moto.Id))
                Items[moto.Id].Quantity++;
            // иначе - добавить объект в корзину
            else
                Items.Add(moto.Id, new CartItem
                {
                    Moto = moto,
                    Quantity = 1
                });
        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }

    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public Moto Item { get; set; }
        public int Qty { get; set; }
    }
}

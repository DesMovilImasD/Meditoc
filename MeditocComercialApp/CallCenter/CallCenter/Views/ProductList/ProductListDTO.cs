using System;
using System.Collections.Generic;
using CallCenter.Helpers;
using Xamarin.Forms;
using CallCenter.Helpers.FontAwesome;
using System.Collections.ObjectModel;
using System.Linq;
using CallCenter.ViewModels;

namespace CallCenter.Views.ProductList
{
    public class ProductListDTO : BaseViewModel, PaymentPageInterface
    {
        
        public static ProductListDTO Create(int id, string name, List<ProductItemDTO> items)
        {
            var item = new ProductListDTO(items);
            item.Id = id;
            item.Name = name;
            return item;
        }

        public ProductListDTO(List<ProductItemDTO> items)
        {
            source.Clear();
            foreach (var e in items)
            {
                e.Render();
                source.Add(e);
            }
            RenderCost();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private double _Total = 0.0;
        public double Total {
            get { return _Total; }
            set
            {
                _Total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private double _SubTotal = 0.0;
        public double SubTotal {
            get { return _SubTotal;  }
            set
            {
                _SubTotal = value;
                OnPropertyChanged(nameof(SubTotal));
            }
        }

        private double _IvaDiscount = 0.0;
        public double IvaDiscount
        {
            get { return _IvaDiscount; }
            set
            {
                _IvaDiscount = value;
                OnPropertyChanged(nameof(IvaDiscount));
            }
        }

        private string _IvaLabel = $"IVA {Settings.IVA}";
        public string IvaLabel
        {
            get { return _IvaLabel; }
            set
            {
                _IvaLabel = value;
                OnPropertyChanged(nameof(IvaLabel));
            }
        }

        private string _RedeemCode = "----";
        public string RedeemCode {
            get { return _RedeemCode; }
            set
            {
                _RedeemCode = value;
                OnPropertyChanged(nameof(RedeemCode));
            }
        }


        public ProductReedemDTO Coupon { get; set; }

        private readonly ObservableCollection<ProductItemDTO> source = new ObservableCollection<ProductItemDTO>();
        public ObservableCollection<ProductItemDTO> Products { get { return source; } }

        /// <summary>
        /// Calcula el costo total del producto, con descuento del cupon.
        /// </summary>
        public void RenderCost()
        {
            Total = 0;
            SubTotal = 0;
            SubTotal = source.Select(o => o.Cost * (int)o.Quantity).Sum();
            double discount = 0.0;
           
            if(Coupon != null )
            {
                var _theshholdCoupon = SubTotal * Settings.ThresholdCouponDiscount;
                if (Coupon.Type == 1) // descuento por monto
                {
                    // hay que validar que el cupon cubra
                    // como maximo el 90% de la compra
                    // y en caso que no lo sea tomar el 10% del total
                    // como pago.
                    // el valor es regresado en centavos, por ese motivo es
                    // necesario dividirlo entre 100
                    var _realValue = Coupon.Discount / 100; 
                    discount = (_theshholdCoupon >= _realValue) ? _realValue : _theshholdCoupon;
                }
                else // descuento por porcentaje.
                {
                    // hay que validar que el cupon solo llegue con no tenga
                    //un valor mayor a 0.90, si es el caso solo cobrar el 10%.
                    discount = (Coupon.Discount > Settings.ThresholdCouponDiscount) ? _theshholdCoupon : SubTotal * Coupon.Discount;
                }
                // calculo obsoleto.
                //discount = Coupon.Type ==1 ?Coupon.Discount :(SubTotal * Coupon.Discount);
                RedeemCode = Coupon.Code;
            }
            // restamos el descuento al subtotal.
            IvaDiscount = (SubTotal - discount) * Settings.IVA;
            Total = (SubTotal - discount) + IvaDiscount;
        }

        /// <summary>
        /// Eliminar un producto y calcular de nuevo el costo total.
        /// </summary>
        /// <param name="item"></param>
        public void Delete(ProductItemDTO item)
        {
            source.Remove(item);
            RenderCost();
        }
    }

    public class ProductItemDTO: BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private double _cost = 0.0;
        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private decimal _quantity = 1;
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private double _total { get; set; } = 0.0;
        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private string _deleteIcon = FontAwesomeIcons.Trash;
        public string DeleteIcon
        {
            get { return _deleteIcon; }
            set
            {
                _deleteIcon = value;
                OnPropertyChanged(nameof(DeleteIcon));
            }
        }

        private string _incrementIcon = FontAwesomeIcons.PlusCircle;
        public string IncrementIcon
        {
            get { return _incrementIcon; }
            set
            {
                _incrementIcon = value;
                OnPropertyChanged(nameof(IncrementIcon));
            }
        }

        private string _decrementIcon = FontAwesomeIcons.MinusCircle;
        public string DecrementIcon
        {
            get { return _decrementIcon; }
            set
            {
                _decrementIcon = value;
                OnPropertyChanged(nameof(DecrementIcon));
            }
        }

        private string _quantityIcon = FontAwesomeIcons.CaretSquareDown;
        public string QuantityIcon
        {
            get { return _quantityIcon; }
            set
            {
                _decrementIcon = value;
                OnPropertyChanged(nameof(QuantityIcon));
            }
        }

        public void Render()
        {
            Total = (float)Quantity * Cost;
        }
    }

    public class ProductReedemDTO
    {
        public string Code { get; set; }
        public double Discount { get; set; }
        public DateTime ExpireIn { get; set; }
        public int Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}


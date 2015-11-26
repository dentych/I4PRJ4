using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using SharedLib.Models;

namespace KasseApparat
{
    public class ShoppingList : ObservableCollection<PurchasedProduct>, INotifyPropertyChanged
    {
        public IDBcontrol _db = new FakeDBcontrol(); //Fake for testing
        //public IDBcontrol _db = new DBcontrol(new Connection("127.0.0.1", 11000));
        public IPrinter print = new FilePrinter();

        /// <summary>
        /// Tilføjer et produkt til shoppinglisten. 
        /// Kontrollere først om produktet allerede er på listen og i så fald incrementeres quantity kun
        /// </summary>
        /// <param name="product"></param>
        public void AddItem(PurchasedProduct product)
        {
            foreach (var prod in this)
                if (prod.Name == product.Name)
                {
                    prod.Quantity += product.Quantity;
                    Notify("TotalPrice");
                    return;
                }
            Add(product);
            Notify("TotalPrice");
        }

        /// <summary>
        /// Incrementere quantity på det valgte purchaseproduct
        /// </summary>
        /// <param name="index"></param>
        public void IncrementQuantity(int index)
        {
            if (index >= 0)
            {
                this[index].Quantity++;
                Notify("TotalPrice");
            }
        }

        /// <summary>
        /// Sætter quantity på det i gui valgte product
        /// </summary>
        /// <param name="ammount"></param>
        public void SetQuantity(int ammount)
        {
            if (CurrentIndex >= 0)
            {
                this[CurrentIndex].Quantity = ammount;
                Notify("TotalPrice");
            }
        }
        
        /// <summary>
        /// Udregner den total pris for vare på shoppinglisten
        /// </summary>
        public int TotalPrice
        {
            get { return this.Sum(vare => (int) vare.TotalPrice); }
        }

        /// <summary>
        /// Afslutter et køb. Sender købet til central server og clear shoppinglist
        /// </summary>
        public void EndPurchase()
        {
            _db.PurchaseDone(this);
            print.PrintPurchase(this.ToList());
            ClearCommand.Execute(this);
        }

#region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifier gui elementer når en given var ændres
        /// </summary>
        /// <param name="propertyName"></param>
        void Notify([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
#endregion

#region Index
        private int _currentIndex = 0;
        /// <summary>
        /// Nuværende valgte element på shoppinglisten
        /// </summary>
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                Notify();
            }
        }
        
#endregion

#region MoreButton
        ICommand _ButtonMoreClick;
        public ICommand MoreCommand { get { return _ButtonMoreClick ?? (_ButtonMoreClick = new RelayCommand(MoreCommandExecute, MoreCommandCanExecute)); } }

        /// <summary>
        /// Incrementere det i shoppinglisten valgte items quantity, og notifier totalprice
        /// </summary>
        private void MoreCommandExecute()
        {
            this[CurrentIndex].Quantity++;
            Notify("TotalPrice");
        }

        /// <summary>
        /// Kontrollere om der er et valgt item som kan få sin quantity incrementeret
        /// </summary>
        /// <returns></returns>
        bool MoreCommandCanExecute()
        {
            if (CurrentIndex < 0)
                return false;
            else
                return true;
        }
#endregion

#region LessButton
        ICommand _ButtonLessClick;
        public ICommand LessCommand { get { return _ButtonLessClick ?? (_ButtonLessClick = new RelayCommand(LessCommandExecute, LessCommandCanExecute)); } }

        /// <summary>
        /// Dekrementere det i shoppinglisten valgte items quantity, og notifier totalprice
        /// </summary>
        private void LessCommandExecute()
        {
            if (this[CurrentIndex].Quantity - 1 == 0)
            {
                RemoveAt(CurrentIndex);
                Notify("TotalPrice");
                return;
            }
            this[CurrentIndex].Quantity--;
            Notify("TotalPrice");
        }

        /// <summary>
        /// Kontrollere om der er et valgt item som kan få sin quantity decrementeret
        /// </summary>
        /// <returns></returns>
        bool LessCommandCanExecute()
        {
            if (CurrentIndex < 0)
                return false;
            else
                return true;
        }
#endregion

#region PrevButton
        ICommand _PrevCommand;
        public ICommand PrevCommand { get { return _PrevCommand ?? (_PrevCommand = new RelayCommand(PrevCommandExecute, PrevCommandCanExecute)); } }

        /// <summary>
        /// Sætter det valgte produkt i shoppinglisten til det foregående 
        /// </summary>
        void PrevCommandExecute()
        {
            CurrentIndex--;
        }

        /// <summary>
        /// Kontrollere om der er et foregående produkt som kan vælges
        /// </summary>
        /// <returns></returns>
        bool PrevCommandCanExecute()
        {
            if (CurrentIndex < 1)
                return false;
            else
                return true;
        }
#endregion

#region NextButton
        ICommand _NextCommand;
        public ICommand NextCommand { get { return _NextCommand ?? (_NextCommand = new RelayCommand(NextCommandExecute, NextCommandCanExecute)); } }

        /// <summary>
        /// Sætter det valgte produkt i shoppinglisten til det efterfølgende
        /// </summary>
        void NextCommandExecute()
        {
            CurrentIndex++;
        }

        /// <summary>
        /// Kontrollere om der er et efterfølgende produkt som kan vælges
        /// </summary>
        /// <returns></returns>
        bool NextCommandCanExecute()
        {
            if (CurrentIndex < (Count - 1))
                return true;
            else
                return false;
        }
#endregion

#region Delete
        ICommand _ButtonDeleteClick;
        public ICommand DeleteCommand { get { return _ButtonDeleteClick ?? (_ButtonDeleteClick = new RelayCommand(DeleteCommandExecute, DeleteCommandCanExecute)); } }

        /// <summary>
        /// Fjerne det valgte produkt helt fra shoppinglisten
        /// </summary>
        private void DeleteCommandExecute()
        {
            RemoveAt(CurrentIndex);
            Notify("TotalPrice");
        }

        /// <summary>
        /// Kontrollere om der er et valgt produkt som kan fjernes
        /// </summary>
        /// <returns></returns>
        bool DeleteCommandCanExecute()
        {
            if (CurrentIndex < 0) return false;
            else return true;
        }
#endregion

#region Clear
        ICommand _ButtonClearClick;
        public ICommand ClearCommand { get { return _ButtonClearClick ?? (_ButtonClearClick = new RelayCommand(ClearCommandExecute, ClearCommandCanExecute)); } }

        /// <summary>
        /// Ryder hele shoppinglisten
        /// </summary>
        private void ClearCommandExecute()
        {
            while (Count > 0)
                RemoveAt(Count-1);
            Notify("TotalPrice");
        }

        /// <summary>
        /// Kontrollere om der er noget på shoppinglisten som kan rydes
        /// </summary>
        /// <returns></returns>
        bool ClearCommandCanExecute()
        {
            if (Count <= 0) return false;
            else return true;
        }
#endregion
    }
}

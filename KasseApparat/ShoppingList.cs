﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class ShoppingList : ObservableCollection<PurchasedProduct>
    {
        public ShoppingList()
        {
            Product p1 = new Product();
            p1.Name = "Beer";
            p1.Price = 12;
            p1.ProductId = 0;
            p1.ProductNumber = "0";

            Product p2 = new Product();
            p2.Name = "Chips";
            p2.Price = 20;
            p2.ProductId = 1;
            p2.ProductNumber = "1";

            Add(new PurchasedProduct(p1, 6));
            Add(new PurchasedProduct(p2, 1));
        }

        public int TotalPrice()
        {
            return this.Sum(vare => (int) vare.TotalPrice);
        }

#region Index
        private int _currentIndex;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                Notify();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName]string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
#endregion

#region MoreButton
        ICommand _ButtonMoreClick;
        public ICommand MoreCommand { get { return _ButtonMoreClick ?? (_ButtonMoreClick = new RelayCommand(MoreCommandExecute, MoreCommandCanExecute)); } }

        private void MoreCommandExecute()
        {
            if (CurrentIndex != -1)
                this[CurrentIndex].Quantity++;
        }

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

        private void LessCommandExecute()
        {
            if (CurrentIndex == -1) return;

            if (this[CurrentIndex].Quantity - 1 == 0)
            {
                RemoveAt(CurrentIndex);
                return;
            }
            this[CurrentIndex].Quantity--;
        }

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

        void PrevCommandExecute()
        {
            CurrentIndex--;
        }

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

        void NextCommandExecute()
        {
            CurrentIndex++;
        }

        bool NextCommandCanExecute()
        {
            if (CurrentIndex < (Count - 1))
                return true;
            else
                return false;
        }
#endregion
    }
}

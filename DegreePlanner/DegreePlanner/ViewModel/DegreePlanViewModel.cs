using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data;
using DAL;
using CommunityToolkit.Mvvm.Input;
using DegreePlanner.View;

namespace DegreePlanner.ViewModel
{
    public partial class DegreePlanViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Term> terms;

        [ObservableProperty]
        public string termName;

        [ObservableProperty]
        public DateTime start;

        [ObservableProperty]
        public DateTime end;

        public DegreePlanViewModel()
        {
            LoadTerms();
        }

        [RelayCommand]
        public void AddTerm()
        {
            var db = new DatabaseHandler();

            db.AddItem<Term>(new Term()
            {
                Name = TermName,
                Start = Start,
                End = End,
                TotalCus = 0
            });

            LoadTerms();
        }

        public void LoadTerms()
        {
            var db = new DatabaseHandler();
            Terms = new ObservableCollection<Term>(db.GetAllTerms());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site_Roll_On_Blb.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Galerie> ItemsGalerie { get; set; }
        public IEnumerable<Publication> ItemsNews { get; set; }
        public IEnumerable<Publication> ItemsEventRealise { get; set; }
        public IEnumerable<Publication> ItemsEventFuture { get; set; }
        public Pager Pager { get; set; }
        public Pager1 Pager1 { get; set; }

    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int Elementpage=4)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)Elementpage);
            var currentPage = page != null ? (int)page : 1;
            var Pagedebut = currentPage - 5;
            var Pagefin = currentPage + 4;
            if (Pagedebut <= 0)
            {
                Pagefin -= (Pagedebut - 1);
                Pagedebut = 1;
            }
            if (Pagefin > totalPages)
            {
                Pagefin = totalPages;
                if (Pagefin > 10)
                {
                    Pagedebut = Pagefin - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            ElementPage = Elementpage;
            TotalPages = totalPages;
            PageDebut = Pagedebut;
            PageFin = Pagefin;

        }
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int ElementPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageDebut { get; private set; }
        public int PageFin { get; private set; }
    }
    public class Pager1
    {
        public Pager1(int totalItems, int? page, int Elementpage = 2)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)Elementpage);
            var currentPage = page != null ? (int)page : 1;
            var Pagedebut = currentPage - 5;
            var Pagefin = currentPage + 4;
            if (Pagedebut <= 0)
            {
                Pagefin -= (Pagedebut - 1);
                Pagedebut = 1;
            }
            if (Pagefin > totalPages)
            {
                Pagefin = totalPages;
                if (Pagefin > 10)
                {
                    Pagedebut = Pagefin - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            ElementPage = Elementpage;
            TotalPages = totalPages;
            PageDebut = Pagedebut;
            PageFin = Pagefin;

        }
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int ElementPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageDebut { get; private set; }
        public int PageFin { get; private set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public CultureInfo Culture { get; private set; }
        public Release Release { get;  private set; }

        public Publisher Publisher { 
            get=>Release.Publisher; 
            set=>Release.Publisher=value; 
        }

        public IAuthorsCollection Authors { get; set; } = new BookAuthorsCollectionWrapper();

        public Book (string title, CultureInfo culture, Release release, ICollection<Author> authors)
            : this(0,title,culture)
        {
            Release = release;
            Authors.AppendMany(authors);
        }   

        private Book(int id, string title, CultureInfo culture)  // Used by EF Core
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
            Id = id;
            Title = title;
            Culture = culture;
            Release = default;
        }

    }
}

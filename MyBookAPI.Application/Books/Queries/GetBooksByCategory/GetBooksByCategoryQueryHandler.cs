﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Models;
using MyBookAPI.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Queries.GetBooksByCategory
{
    public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, BooksVm>
    {
        private readonly IMyBookDbContext _context;
        public GetBooksByCategoryQueryHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<BooksVm> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.Where(x => x.Category.Equals(request.Category)).ToListAsync(cancellationToken);

            var booksDtoList = new List<BookDto>();

            books.ForEach(c =>
            {
                booksDtoList.Add(new BookDto
                {
                    Name = c.Name,
                    Author = c.Author.AuthorName.ToString(),
                    Category = c.Category.Name,
                    Description = c.Description.Text,
                    Pages = c.Pages,
                    PublishingHouse = c.PublishingHouse.Name,
                    Price = c.Price != 0 ? c.Price : null,
                    ToBeSold = c.ToBeSold
                });
            });

            return new BooksVm
            {
                Books = booksDtoList
            };
        }
    }
}
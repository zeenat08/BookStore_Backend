﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModel
{
    public class WishlistModel
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}

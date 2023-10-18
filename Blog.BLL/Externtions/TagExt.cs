using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Externtions
{
    public static class TagExt
    {
        public static Tag Edit(this Tag tag, Tag updateTag)
        {
            tag.Name = updateTag.Name;
            tag.Articles = updateTag.Articles;

            return tag;
        }
    }
}

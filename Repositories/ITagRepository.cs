using System.Collections.Generic;
using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
    }
}
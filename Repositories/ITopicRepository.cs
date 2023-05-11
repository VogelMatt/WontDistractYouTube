using System.Collections.Generic;
using WontDistractYouTube.Models;

namespace WontDistractYouTube.Repositories
{
    public interface ITopicRepository
    {
        List<Topic> GetAllTopics();
    }
}
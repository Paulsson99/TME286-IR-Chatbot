using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class MovieConversations
    {
        private List<MovieConversationItem> conversations;

        public MovieConversations()
        {
            conversations = new List<MovieConversationItem>();
        }

        public void Add(string rawConversation)
        {
            MovieConversationItem conversationItem = new MovieConversationItem(rawConversation);
            conversations.Add(conversationItem);
        }

        public void MapIdToUtterance(MovieLines movieLines)
        {
            foreach (MovieConversationItem conversation in conversations)
            {
                conversation.MapIdToUtterance(movieLines);
            }
        }

        public List<MovieConversationItem> Conversations { get { return conversations; } }
    }
}

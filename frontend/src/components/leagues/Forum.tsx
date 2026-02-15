
import React, { useState } from 'react';
import { MessageSquare, Send } from 'lucide-react';

// Anpassad post-typ, kan uppdateras när backend är klar
export interface LeaguePostDto {
  postId: number | string;
  userId?: number;
  username: string;
  content: string;
  createdAt: string; // ISO string
}
interface ForumProps {
  posts: LeaguePostDto[];
  onAddPost: (content: string) => Promise<void>;
}

export const Forum: React.FC<ForumProps> = ({ posts, onAddPost }) => {
  const [newPost, setNewPost] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newPost.trim()) return;
    try {
      await onAddPost(newPost);
      setNewPost('');
    } catch (err) {
      alert("Kunde inte skicka inlägg.");
    }
  };

  return (
    <div className="bg-white rounded-2xl shadow-sm border border-slate-200 flex flex-col h-[650px] animate-fade-in">
      <div className="p-6 border-b border-slate-100 flex items-center gap-2">
        <MessageSquare className="text-accent" />
        <h2 className="text-xl font-bold">Klotterplank</h2>
      </div>
      
      <div className="flex-1 overflow-y-auto p-6 space-y-6 scrollbar-thin scrollbar-thumb-slate-200">
        {posts.length > 0 ? posts.map(post => (
          <div key={post.postId} className="flex gap-3 animate-fade-in">
            <div className="w-8 h-8 rounded-full bg-slate-200 flex-shrink-0 flex items-center justify-center font-bold text-xs text-slate-600 border border-slate-300">
              {post.username.charAt(0).toUpperCase()}
            </div>
            <div className="flex-1">
              <div className="bg-slate-50 p-3 rounded-2xl rounded-tl-none border border-slate-100 shadow-sm relative">
                <p className="text-[10px] font-bold text-slate-500 mb-1">{post.username}</p>
                <p className="text-sm text-slate-800 leading-relaxed">{post.content}</p>
              </div>
              <span className="text-[9px] text-slate-400 ml-2 mt-1 block font-medium">
                {new Date(post.createdAt).toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'})} • {new Date(post.createdAt).toLocaleDateString()}
              </span>
            </div>
          </div>
        )) : (
          <div className="h-full flex flex-col items-center justify-center text-slate-400 space-y-2">
            <MessageSquare size={32} className="opacity-20" />
            <p className="text-sm italic">Inga inlägg ännu...</p>
          </div>
        )}
      </div>

      <div className="p-4 border-t border-slate-100 bg-slate-50">
        <form onSubmit={handleSubmit} className="flex gap-2">
          <input 
            className="flex-1 bg-white border border-slate-200 rounded-full px-4 py-2.5 text-sm focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none shadow-inner text-slate-900 font-medium"
            placeholder="Skriv ett meddelande..."
            value={newPost}
            onChange={e => setNewPost(e.target.value)}
          />
          <button type="submit" className="bg-accent text-white p-2.5 rounded-full hover:bg-emerald-600 transition shadow-lg shadow-accent/20 active:scale-95">
            <Send size={18} />
          </button>
        </form>
      </div>
    </div>
  );
};

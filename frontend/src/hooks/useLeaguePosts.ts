import { useState, useEffect } from 'react';

// Mock-typ för Post
export type Post = {
  id: string | number;
  username: string;
  content: string;
  createdAt: string;
};

// Mock-data
const mockPosts: Post[] = [
  { id: 1, username: 'Alice', content: 'Hej alla!', createdAt: new Date().toISOString() },
  { id: 2, username: 'Bob', content: 'Vi behöver fler lag!', createdAt: new Date().toISOString() }
];

// Mock-funktioner som ersätter API-anrop
const getLeaguePosts = async (leagueId: string): Promise<Post[]> => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(mockPosts);
    }, 300);
  });
};

const createPost = async (leagueId: string, content: string): Promise<Post> => {
  return new Promise((resolve) => {
    setTimeout(() => {
      const newPost: Post = {
        id: Date.now(),
        username: 'Du', // mockar användaren
        content,
        createdAt: new Date().toISOString()
      };
      resolve(newPost);
    }, 300);
  });
};

export const useLeaguePosts = (id?: string) => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    getLeaguePosts(id).then((data) => {
      setPosts(data);
      setLoading(false);
    });
  }, [id]);

  const addPost = async (content: string) => {
    if (!id || !content.trim()) return;
    try {
      const p = await createPost(id, content);
      setPosts((prev) => [p, ...prev]);
      return true;
    } catch (err) {
      console.error(err);
      return false;
    }
  };

  return { posts, loading, addPost };
};
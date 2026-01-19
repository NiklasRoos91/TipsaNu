import { useState, useEffect } from 'react';
import { Post } from '../types/types';
import { getLeaguePosts, createPost } from '../services/api';

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
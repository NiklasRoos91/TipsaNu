import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './useAuth';

interface Credentials {
  email: string;
  password: string;
}

export const useLogin = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const { login, error: authError, loading: authLoading, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated) {
      navigate('/tournaments');
    }
  }, [isAuthenticated, navigate]);

  const handleLogin = async (e?: React.FormEvent, credentials?: Credentials) => {
    if (e) e.preventDefault();

    const loginData: Credentials = credentials
      ? credentials
      : { email: email, password: password };

    try {
      await login(loginData);
    } catch (err) {
      console.error('Login failed:', err);
    }
  };

  const quickLogin = (type: 'user' | 'admin') => {
    const creds = type === 'admin' 
      ? { email: 'admin@admin.com', password: 'admin123' } 
      : { email: 'user@user.com', password: 'user123' };
    
    setEmail(creds.email);
    setPassword(creds.password);
    handleLogin(undefined, creds);
  };

  return {
    email,
    setEmail,
    password,
    setPassword,
    error: authError,
    isLoggingIn: authLoading,
    handleLogin,
    quickLogin
  };
};
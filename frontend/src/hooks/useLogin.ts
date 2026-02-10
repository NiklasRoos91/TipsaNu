import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './useAuth';

interface Credentials {
  Email: string;
  Password: string;
}

export const useLogin = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const { login, error: authError, loading: authLoading, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async (e?: React.FormEvent, credentials?: Credentials) => {
    if (e) e.preventDefault();

    const loginData: Credentials = credentials
      ? credentials
      : { Email: email, Password: password };

    try {
      await login(loginData);

      console.log('isAuthenticated efter login:', isAuthenticated);
      
      navigate('/tournaments');
    } catch (err) {
      console.error('Login failed:', err);
    }
  };

  const quickLogin = (type: 'user' | 'admin') => {
    const creds = type === 'admin' 
      ? { Email: 'admin@admin.com', Password: 'Password123!' } 
      : { Email: 'user@user.com', Password: 'Password123!' };
    
    setEmail(creds.Email);
    setPassword(creds.Password);
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
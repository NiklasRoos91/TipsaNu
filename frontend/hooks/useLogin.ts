import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './useAuth';

export const useLogin = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isLoggingIn, setIsLoggingIn] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async (e?: React.FormEvent, credentials?: { email: string; pass: string }) => {
    if (e) e.preventDefault();
    setError('');
    setIsLoggingIn(true);

    const loginEmail = credentials ? credentials.email : email;
    const loginPass = credentials ? credentials.pass : password;

    try {
      await login(loginEmail, loginPass);
      navigate('/tournaments');
    } catch (err) {
      setError('Felaktiga inloggningsuppgifter. Prova igen.');
    } finally {
      setIsLoggingIn(false);
    }
  };

  const quickLogin = (type: 'user' | 'admin') => {
    const creds = type === 'admin' 
      ? { email: 'admin', pass: 'password' } 
      : { email: 'user1', pass: 'password' };
    
    setEmail(creds.email);
    setPassword(creds.pass);
    handleLogin(undefined, creds);
  };

  return {
    email,
    setEmail,
    password,
    setPassword,
    error,
    isLoggingIn,
    handleLogin,
    quickLogin
  };
};
import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { User, AuthResponse, LoginRequest, RegisterRequest } from '../types/authTypes';
import * as api from '../services/authService';

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (data: LoginRequest) => Promise<void>;
  register: (data: RegisterRequest) => Promise<void>;
  updateUser: (data: Partial<User>) => Promise<void>;
  logout: () => Promise<void>;
  isAuthenticated: boolean;
  loading: boolean;
  error: string | null;
}

export const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export const AuthProvider = ({ children }: { children?: ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(!!localStorage.getItem('token'));


  const getErrorMessage = (err: any, fallback: string): string => {
    if (err.response?.data?.errorMessages?.length > 0) {
      return err.response.data.errorMessages[0];
    }
    return err.response?.data?.errorMessage || err.response?.data?.message || fallback;
  };

  useEffect(() => {
    const initAuth = async () => {
      const storedToken = localStorage.getItem('token');
      if (storedToken) {
        try {
          const currentUser  = await api.verifyToken();

          if (currentUser ) {
            setUser(currentUser );
            setToken(localStorage.getItem('token'));
            setIsAuthenticated(true);
          }
        } catch (err) {
          setUser(null);
          setIsAuthenticated(false);
          setToken(null);
          localStorage.removeItem('token');
          localStorage.removeItem('refreshToken');
        }
    }
      setLoading(false);
    };
    initAuth();
  }, []);

  // --- Auth actions ---
  // Login
  const login = async (data: LoginRequest) => {
    setLoading(true);
    setError(null);
    try {
      const res: AuthResponse & { user: User } = await api.login(data);
      localStorage.setItem('token', res.accessToken);
      localStorage.setItem('refreshToken', res.refreshToken);
      setToken(res.accessToken);
      setUser(res.user);
      setIsAuthenticated(true);
    } catch (err:any) {
      setIsAuthenticated(false);
      setError(getErrorMessage(err, 'Felaktiga inloggningsuppgifter'));
      throw err;
    } finally {
      setLoading(false);
    }
  };

  // Register
  const register = async (data: RegisterRequest) => {
    setLoading(true);
    setError(null);
    try {
      const res: AuthResponse & { user: User } = await api.register(data);
      localStorage.setItem('token', res.accessToken);
      localStorage.setItem('refreshToken', res.refreshToken);
      setUser(res.user);
      setToken(res.accessToken);
      setIsAuthenticated(true);
    } catch (err: any) {
      setIsAuthenticated(false);
      setError(getErrorMessage(err, 'Registrering misslyckades'));
      throw err;
    } finally {
      setLoading(false);
    }
  };

  // Update user
  const updateUser = async (data: Partial<User>) => {
    setLoading(true);
    setError(null);
    try {
      if (!token) throw new Error('Inte autentiserad');
      const updatedUser = await api.updateProfile(data);
      setUser(updatedUser);
    } catch (err: any) {
      setError(getErrorMessage(err, 'Uppdatering misslyckades'));
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const logout =  async() => {
    const refresh = localStorage.getItem('refreshToken');

    if (refresh) {
      try {
        await api.logout({ refreshToken: refresh });
      } catch (err: any) {
        const msg = getErrorMessage(err, 'Kunde inte logga ut från servern');
        console.error("Kunde inte rensa token i databasen:", msg);      
      }
    }

    setUser(null);
    setToken(null);
    setIsAuthenticated(false);
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  };

  return (
    <AuthContext.Provider
     value={{
      user,
      token,
      login,
      register,
      updateUser,
      logout,
      isAuthenticated,
      loading,
      error
    }}>
      {children}
    </AuthContext.Provider>
  );
};
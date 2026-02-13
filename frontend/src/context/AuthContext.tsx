import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { User, AuthResponse, LoginRequest, RegisterRequest } from '../types/authTypes';
import * as api from '../services/authService';

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (data: LoginRequest) => Promise<void>;
  register: (data: RegisterRequest) => Promise<void>;
  updateUser: (data: Partial<User>) => Promise<void>;
  logout: () => void;
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

  //Init auth on page load/refresh
  useEffect(() => {
    const initAuth = async () => {
      const storedToken = localStorage.getItem('token');
      if (storedToken) {
        try {
          const currentUser  = await api.verifyToken();  //backend returns user from token
          console.log("verifyToken response:", currentUser);  
          
          if (currentUser ) {
            setUser(currentUser );
            setToken(storedToken);
            console.log("User set in AuthProvider:", currentUser); // <-- logg här
            console.log("Is admin?", currentUser.role === "Admin"); // <-- lo
          } else {
            localStorage.removeItem('token');
          }
        } catch (err) {
          console.log('Token invalid eller expired', err);
          localStorage.removeItem('token');
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
    setError('');
    try {
      const res: AuthResponse & { user: User } = await api.login(data);
      setUser(res.user);
      setToken(res.accessToken);
      localStorage.setItem('token', res.accessToken);
      localStorage.setItem('refreshToken', res.refreshToken);
    } catch (err:any) {
      setError(err.response?.data?.message || 'Felaktiga inloggningsuppgifter');
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
      setUser(res.user);
      setToken(res.accessToken);
      localStorage.setItem('token', res.accessToken);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Registrering misslyckades');
      throw err; // Rethrow to allow component-level handling if needed
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
      setError(err.response?.data?.message || 'Uppdatering misslyckades');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const logout = () => {
    setUser(null);
    setToken(null);
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
      isAuthenticated: !!token,
      loading,
      error
    }}>
      {children}
    </AuthContext.Provider>
  );
};
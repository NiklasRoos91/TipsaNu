import { api } from './apiClient';
import { User, AuthResponse, LoginRequest, RegisterRequest, RefreshTokenRequest } from '../types/authTypes';

export const login = async (data: LoginRequest): Promise<AuthResponse  & { user: User   }> => {
  const res = await api.post<AuthResponse & { user: User }>('/auth/login', data);
  return res.data;
};

export const register = async (data: RegisterRequest): Promise<AuthResponse & { user: User }> => {
  const res = await api.post<AuthResponse & { user: User }>('/auth/register', data);
  return res.data;
};

export const verifyToken = async (): Promise<User> => {
  const res = await api.get<User>('/auth/me');
  return res.data;
};

export const updateProfile = async (data: Partial<User>): Promise<User> => {
  const res = await api.put<User>('/auth/profile', data);
  return res.data;
};

export const refreshToken = async (data: RefreshTokenRequest): Promise<AuthResponse> => {
  const res = await api.post<AuthResponse>('/auth/refresh-token', data);
  return res.data;
};

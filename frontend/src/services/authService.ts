import { api } from './apiClient';
import { jwtDecode } from 'jwt-decode';
import { User, AuthResponse, LoginRequest, RegisterRequest, RefreshTokenRequest, TokenPayload } from '../types/authTypes';

// --- Auth API calls ---
// ---Login---
export const login = async (data: LoginRequest): Promise<AuthResponse  & { user: User   }> => {
  const res = await api.post<AuthResponse & { user: User }>('/auth/login', data);
  return res.data;
};

// ---Register---
export const register = async (data: RegisterRequest): Promise<AuthResponse & { user: User }> => {
  const res = await api.post<AuthResponse & { user: User }>('/auth/register', data);
  return res.data;
};

// --- Verify Token & Get Current User ---
export const verifyToken = async (): Promise<User> => {
  const res = await api.get<User>('/auth/me');
  return res.data;
};

// --- Update Profile ---
export const updateProfile = async (data: Partial<User>): Promise<User> => {
  const res = await api.put<User>('/auth/profile', data);
  return res.data;
};

// --- Refresh Token ---
export const refreshToken = async (data: RefreshTokenRequest): Promise<AuthResponse> => {
  const res = await api.post<AuthResponse>('/auth/refresh-token', data);
  return res.data;
};

// --- Utility to get user role from token ---
export const getUserRoleFromToken = (token: string): string | null => {
  try {
    const payload: any = jwtDecode(token);
    return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
  } catch {
    return null;
  }
};

// --- Utility to check if user is admin ---
export const isAdmin = (token: string): boolean => getUserRoleFromToken(token) === "Admin";

import axios from 'axios';
import { refreshToken as refreshTokenApi } from './authService';


export const api = axios.create({
  baseURL: 'https://localhost:54877/api',
  headers: { 'Content-Type': 'application/json' },
});

api.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

api.interceptors.response.use(
  res => res,
  async error => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      const refresh = localStorage.getItem('refreshToken');
      if (!refresh) return Promise.reject(error);

      try {
        const data = await refreshTokenApi({ refreshToken: refresh });

        localStorage.setItem('token', data.accessToken);
        localStorage.setItem('refreshToken', data.refreshToken);

        originalRequest.headers.Authorization = `Bearer ${data.accessToken}`;
        return api(originalRequest);
      } catch (e) {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        return Promise.reject(e);
      }
    }

    return Promise.reject(error);
  }
);
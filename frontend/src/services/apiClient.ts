import axios from 'axios';
import { refreshToken as refreshTokenApi } from './authService';


export const api = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api`,
  headers: { 'Content-Type': 'application/json' },
});

let isRefreshing = false;
let failedQueue: any[] = [];
const processQueue = (error: any, token: string | null = null) => {
  failedQueue.forEach(prom => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });
  failedQueue = [];
};

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

      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        })
          .then(token => {
            originalRequest.headers.Authorization = `Bearer ${token}`;
            return api(originalRequest);
          })
          .catch(err => {
            return Promise.reject(err);
          });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      const refresh = localStorage.getItem('refreshToken');
      if (!refresh) return Promise.reject(error);

      try {
        const response = await axios.post(`${import.meta.env.VITE_API_URL}/api/auth/refresh`, {
          refreshToken: refresh
        }, {
          headers: { 'Content-Type': 'application/json' }
        });

        const data = response.data;
        localStorage.setItem('token', data.accessToken);
        localStorage.setItem('refreshToken', data.refreshToken);

        originalRequest.headers.Authorization = `Bearer ${data.accessToken}`;

        processQueue(null, data.accessToken);
        
        isRefreshing = false;
        return api(originalRequest);
      } catch (e) {
        localStorage.removeItem('token');
        processQueue(e, null);
        isRefreshing = false;
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        window.location.href = '/login';
        return Promise.reject(e);
      }
    }

    return Promise.reject(error);
  }
);
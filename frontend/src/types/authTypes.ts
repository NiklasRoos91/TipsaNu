// --- User (frontend-version) ---
export interface User {
  id: string;
  username: string;
  email: string;
  displayName: string;
  avatarUrl?: string;
  points: number;
  bio?: string;
  role?: 'User' | 'Admin';
  createdAt?: string;
}

// --- Auth Responses ---
export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
}

// --- Auth Requests ---
export interface RefreshTokenRequest {
  refreshToken: string;
}

export interface RegisterRequest {
  Email: string;
  Username: string;
  Password: string;
}

export interface LoginRequest {
  Email: string;
  Password: string;
}
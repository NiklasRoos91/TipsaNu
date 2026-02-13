import { useContext, useMemo } from 'react';
import { AuthContext } from '../context/AuthContext';
import { getUserRoleFromToken  } from '../services/authService';

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }

  const isAdmin = useMemo(() => {
    if (context.token) {
      return getUserRoleFromToken(context.token) === "Admin";
    }
    return false;
  }, [context.token]);

  console.log("useAuth hook - isAdmin:", isAdmin);
  return {
  ...context,
  isAdmin
  };
};
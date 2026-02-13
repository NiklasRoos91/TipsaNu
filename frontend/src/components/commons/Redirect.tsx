import React from 'react';
import { Navigate } from 'react-router-dom';

interface RedirectProps {
  to: string;
  replace?: boolean;
}

export const Redirect: React.FC<RedirectProps> = ({ to, replace = true }) => {
  return <Navigate to={to} replace={replace} />;
};

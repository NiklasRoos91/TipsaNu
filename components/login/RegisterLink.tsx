import React from 'react';
import { Link } from 'react-router-dom';

export const RegisterLink = () => {
  return (
    <div className="mt-6 text-center text-sm text-slate-500">
      Inget konto?{' '}
      <Link to="/register" className="text-accent hover:underline font-medium">
        Registrera dig här
      </Link>
    </div>
  );
};
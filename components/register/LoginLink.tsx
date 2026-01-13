import React from 'react';
import { Link } from 'react-router-dom';

export const LoginLink: React.FC = () => {
  return (
    <div className="mt-6 text-center">
      <Link to="/login" className="text-sm text-slate-500 hover:text-accent font-medium transition-colors">
        Redan medlem? <span className="text-accent hover:underline">Logga in</span>
      </Link>
    </div>
  );
};
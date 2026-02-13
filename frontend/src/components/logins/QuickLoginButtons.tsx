import React from 'react';
import { User, Shield } from 'lucide-react';

interface QuickLoginProps {
  onQuickLogin: (type: 'user' | 'admin') => void;
  isLoggingIn: boolean;
}

export const QuickLoginButtons: React.FC<QuickLoginProps> = ({ onQuickLogin, isLoggingIn }) => {
  return (
    <div className="mt-8 pt-6 border-t border-slate-100">
      <p className="text-center text-xs text-slate-400 mb-4 uppercase tracking-wider font-semibold">Snabbknappar (Mock)</p>
      <div className="grid grid-cols-2 gap-4">
        <button
          type="button"
          onClick={() => onQuickLogin('user')}
          disabled={isLoggingIn}
          className="flex items-center justify-center gap-2 px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 text-sm font-medium rounded-lg transition-colors border border-slate-200 active:scale-95"
        >
          <User size={16} />
          User
        </button>
        <button
          type="button"
          onClick={() => onQuickLogin('admin')}
          disabled={isLoggingIn}
          className="flex items-center justify-center gap-2 px-4 py-2 bg-purple-50 hover:bg-purple-100 text-purple-700 text-sm font-medium rounded-lg transition-colors border border-purple-100 active:scale-95"
        >
          <Shield size={16} />
          Admin
        </button>
      </div>
    </div>
  );
};
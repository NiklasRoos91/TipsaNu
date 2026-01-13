
import React from 'react';

interface LoginFormProps {
  email: string;
  setEmail: (val: string) => void;
  password: string;
  setPassword: (val: string) => void;
  onSubmit: (e: React.FormEvent) => void;
  isLoggingIn: boolean;
}

export const LoginForm: React.FC<LoginFormProps> = ({
  email,
  setEmail,
  password,
  setPassword,
  onSubmit,
  isLoggingIn,
}) => {
  return (
    <form onSubmit={onSubmit} className="space-y-5">
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">Användarnamn eller E-post</label>
        <input
          type="text"
          required
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all text-slate-900 font-medium placeholder:text-slate-300 shadow-inner"
          placeholder="T.ex. user1"
        />
      </div>
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">Lösenord</label>
        <input
          type="password"
          required
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all text-slate-900 font-medium placeholder:text-slate-300 shadow-inner"
          placeholder="••••••••"
        />
      </div>
      <button
        type="submit"
        disabled={isLoggingIn}
        className="w-full bg-accent hover:bg-emerald-600 text-white font-bold py-3.5 rounded-xl transition-all shadow-lg shadow-accent/20 disabled:opacity-70 active:scale-[0.98] mt-2 uppercase tracking-widest text-sm"
      >
        {isLoggingIn ? 'Loggar in...' : 'Logga In'}
      </button>
    </form>
  );
};

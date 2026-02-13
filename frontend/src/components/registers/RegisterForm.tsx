
import React from 'react';

interface RegisterFormProps {
  formData: {
    username: string;
    email: string;
    password: string;
    confirmPassword: string;
  };
  onUpdateField: (field: any, value: string) => void;
  onSubmit: (e: React.FormEvent) => void;
  isRegistering: boolean;
}

export const RegisterForm: React.FC<RegisterFormProps> = ({
  formData,
  onUpdateField,
  onSubmit,
  isRegistering,
}) => {
  return (
    <form onSubmit={onSubmit} className="space-y-4">
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">Användarnamn</label>
        <input
          type="text"
          placeholder="Användarnamn"
          required
          value={formData.username}
          onChange={(e) => onUpdateField('username', e.target.value)}
          className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all text-slate-900 font-medium shadow-inner"
        />
      </div>
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">E-post</label>
        <input
          type="email"
          placeholder="din@epost.se"
          required
          value={formData.email}
          onChange={(e) => onUpdateField('email', e.target.value)}
          className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all text-slate-900 font-medium shadow-inner"
        />
      </div>
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">Lösenord</label>
        <input
          type="password"
          placeholder="••••••••"
          required
          value={formData.password}
          onChange={(e) => onUpdateField('password', e.target.value)}
          className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all text-slate-900 font-medium shadow-inner"
        />
      </div>
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">Bekräfta lösenord</label>
        <input
          type="password"
          placeholder="••••••••"
          required
          value={formData.confirmPassword}
          onChange={(e) => onUpdateField('confirmPassword', e.target.value)}
          className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all text-slate-900 font-medium shadow-inner"
        />
      </div>
      <button
        type="submit"
        disabled={isRegistering}
        className="w-full bg-accent hover:bg-emerald-600 text-white font-bold py-3.5 rounded-xl transition-all shadow-lg shadow-accent/20 active:scale-[0.98] disabled:opacity-50 mt-4 uppercase tracking-widest text-sm"
      >
        {isRegistering ? 'Skapar konto...' : 'Registrera'}
      </button>
    </form>
  );
};

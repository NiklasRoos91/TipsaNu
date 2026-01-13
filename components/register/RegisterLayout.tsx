import React from 'react';

interface RegisterLayoutProps {
  children: React.ReactNode;
}

export const RegisterLayout: React.FC<RegisterLayoutProps> = ({ children }) => {
  return (
    <div className="min-h-screen bg-slate-900 flex items-center justify-center p-4">
      <div className="bg-white rounded-xl shadow-2xl p-8 w-full max-w-md">
        <h1 className="text-3xl font-bold text-center text-primary mb-2">Skapa konto</h1>
        <p className="text-center text-slate-500 mb-6 font-medium">Gå med i TipsaNu idag</p>
        {children}
      </div>
    </div>
  );
};
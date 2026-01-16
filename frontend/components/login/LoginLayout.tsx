import React from 'react';

export const LoginLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="min-h-screen bg-slate-900 flex items-center justify-center p-4">
      <div className="bg-white rounded-xl shadow-2xl p-8 w-full max-w-md">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-primary mb-2">TipsaNu</h1>
          <p className="text-slate-500">Logga in för att börja tippa</p>
        </div>
        {children}
      </div>
    </div>
  );
};
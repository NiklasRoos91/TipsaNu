import React from 'react';

export const LockedMessage: React.FC = () => (
  <div className="text-center bg-red-50 p-6 rounded-xl border border-red-100">
    <p className="text-red-500 font-bold text-lg">Tippning stängd</p>
    <p className="text-red-400">Det är för sent att tippa denna match.</p>
  </div>
);
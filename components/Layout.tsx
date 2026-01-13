
import React, { useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import { Menu, X, Trophy, User, Bell, LogOut, Layout as LayoutIcon } from 'lucide-react';

export const Layout = ({ children }: { children?: React.ReactNode }) => {
  const { user, logout } = useAuth();
  const [isSidebarOpen, setSidebarOpen] = useState(false);
  const location = useLocation();
  const navigate = useNavigate();

  const isAdmin = user?.username === 'admin';

  // Build nav items dynamically
  const navItems = [
    { label: 'Turneringar', path: '/tournaments', icon: Trophy },
  ];

  if (isAdmin) {
    navItems.push({ label: 'Turneringsmallar', path: '/admin/templates', icon: LayoutIcon });
  }

  navItems.push(
    { label: 'Profil', path: '/profile', icon: User },
    { label: 'Notiser', path: '/notifications', icon: Bell }
  );

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <div className="h-screen w-full flex flex-col md:flex-row overflow-hidden bg-slate-50">
      {/* Mobile Header */}
      <div className="md:hidden bg-primary text-white p-4 flex justify-between items-center z-50 flex-shrink-0 shadow-lg">
        <h1 className="text-xl font-bold tracking-tight">TipsaNu</h1>
        <button 
          onClick={() => setSidebarOpen(!isSidebarOpen)} 
          className="p-2 hover:bg-white/10 rounded-lg transition-colors"
        >
          {isSidebarOpen ? <X size={24} /> : <Menu size={24} />}
        </button>
      </div>

      {/* Sidebar Navigation */}
      <aside className={`
        fixed inset-y-0 left-0 transform ${isSidebarOpen ? 'translate-x-0' : '-translate-x-full'}
        md:relative md:translate-x-0 transition-all duration-300 ease-in-out
        w-72 bg-primary text-slate-300 z-40 flex flex-col border-r border-white/5 shadow-2xl h-full
      `}>
        {/* Top: Logo */}
        <div className="flex-shrink-0 p-8 hidden md:block">
          <h1 className="text-2xl font-black text-white tracking-tighter flex items-center gap-2">
            <div className="w-8 h-8 bg-accent rounded-lg flex items-center justify-center">
              <Trophy size={18} className="text-primary" />
            </div>
            TipsaNu
          </h1>
          <p className="text-[10px] font-bold text-slate-400 mt-2 uppercase tracking-[0.2em]">Premium Tippning</p>
        </div>

        {/* User Info */}
        <div className="flex-shrink-0 px-6 py-4 mx-4 mb-6 rounded-2xl bg-white/5 border border-white/5 flex items-center gap-4">
          <div className="relative">
            <img 
              src={user?.avatarUrl || `https://ui-avatars.com/api/?name=${user?.displayName}&background=random`} 
              alt="Avatar" 
              className="w-12 h-12 rounded-full border-2 border-accent object-cover bg-slate-800 shadow-inner" 
            />
          </div>
          <div className="min-w-0">
            <p className="text-white font-bold text-sm truncate">{user?.displayName}</p>
            <p className="text-accent text-xs font-black tracking-wide">{user?.points} <span className="text-[10px] opacity-70">P</span></p>
          </div>
        </div>

        {/* Navigation */}
        <nav className="flex-1 overflow-y-auto px-4 space-y-6 py-2">
          {/* Main Menu */}
          <div className="space-y-1">
            <div className="px-4 mb-2">
              <p className="text-[10px] font-bold text-slate-500 uppercase tracking-widest">Meny</p>
            </div>
            {navItems.map((item) => {
              const Icon = item.icon;
              const isActive = location.pathname === item.path || (item.path === '/admin/templates' && location.pathname.startsWith('/admin/templates'));
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  onClick={() => setSidebarOpen(false)}
                  className={`flex items-center gap-3 px-4 py-3 rounded-xl transition-all duration-200 group ${
                    isActive ? 'bg-accent text-primary font-bold shadow-lg' : 'hover:bg-white/5 hover:text-white'
                  }`}
                >
                  <Icon size={18} className={isActive ? 'text-primary' : 'text-slate-500 group-hover:text-accent'} />
                  <span className="text-sm">{item.label}</span>
                </Link>
              );
            })}
          </div>
        </nav>

        {/* Logout */}
        <div className="flex-shrink-0 p-6 mt-auto">
          <button
            onClick={handleLogout}
            className="flex items-center gap-3 px-4 py-3 w-full text-left text-red-400 hover:bg-red-500/10 rounded-xl transition-all font-bold text-sm"
          >
            <LogOut size={18} />
            <span>Logga ut</span>
          </button>
        </div>
      </aside>

      {/* Overlay */}
      {isSidebarOpen && (
        <div 
          className="fixed inset-0 bg-primary/60 backdrop-blur-md z-30 md:hidden"
          onClick={() => setSidebarOpen(false)}
        />
      )}

      {/* Main Content */}
      <main className="flex-1 h-full overflow-y-auto bg-slate-50/50">
        <div className="max-w-7xl mx-auto p-4 md:p-10 lg:p-12">
          {children}
        </div>
      </main>
    </div>
  );
};

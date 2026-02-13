import { useLogin } from '../../hooks/useLogin';

export const LoginForm = () => {
  const { email, setEmail, password, setPassword, error, isLoggingIn, handleLogin, quickLogin } = useLogin();

  return (
    <form onSubmit={handleLogin} className="space-y-5">
      <div>
        <label htmlFor="email" className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">
          E-post
          </label>
        <input
          id="email"
          type="text"
          required
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all text-slate-900 font-medium placeholder:text-slate-300 shadow-inner"
          placeholder="E-postadress"
        />
      </div>

      <div>
        <label htmlFor="password" className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">
          Lösenord
          </label>
        <input
          id="password"
          type="password"
          required
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all text-slate-900 font-medium placeholder:text-slate-300 shadow-inner"
          placeholder="••••••••"
        />
      </div>

      {error && <p className="text-red-500 text-sm font-medium">{error}</p>}

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

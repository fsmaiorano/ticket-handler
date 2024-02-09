import { AppContext } from '@/contexts/app-context';
import { useContext } from 'react';
import { Helmet } from 'react-helmet-async';

export function Dashboard() {
  const { user } = useContext(AppContext)

  return (
    <>
      <Helmet title="Dashboard" />
      <div className="flex flex-col gap-4">
        <h1 className="text-3xl font-bold tracking-tight">Dashboard</h1>
        <p>{user.name}</p>
      </div>
    </>
  )
}

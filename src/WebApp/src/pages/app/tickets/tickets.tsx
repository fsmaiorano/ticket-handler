import { AppContext } from '@/contexts/app-context';
import { useContext } from 'react';
import { Helmet } from 'react-helmet-async';

export function Tickets() {
  const { user } = useContext(AppContext)

  return (
    <>
      <Helmet title="Tickets" />
      <div className="flex flex-col gap-4">
        <h1 className="text-3xl font-bold tracking-tight">Tickets</h1>
        <p>{user.name}</p>
      </div>
    </>
  )
}

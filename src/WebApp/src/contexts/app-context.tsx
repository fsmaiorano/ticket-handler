import { Holder } from '@/models/holder'
import { Sector } from '@/models/sector'
import { User } from '@/models/user'
import { createContext, useState } from 'react'

interface IAppContextProps {
  children: React.ReactNode
}

interface IAppContext {
  user: User
  users: User[]
  holder: Holder
  sectors: Sector[]
  token: string
  userHandler: (user: User) => void
  usersHandler: (users: User[]) => void
  tokenHandler: (token: string) => void
  holderHandler: (holder: Holder) => void
  sectorsHandler: (sectors: Sector[]) => void
}

export const AppContext = createContext({} as IAppContext)

export function AppContextProvider({ children }: IAppContextProps) {
  const [user, setUser] = useState<User>({} as User)
  const [users, setUsers] = useState<User[]>([] as User[])
  const [token, setToken] = useState<string>({} as string)
  const [holder, setHolder] = useState<Holder>({} as Holder)
  const [sectors, setSectors] = useState<Sector[]>([] as Sector[])

  function userHandler(user: User) {
    user && setUser(user)
  }

  function tokenHandler(token: string) {
    token && setToken(token)
  }

  function holderHandler(holder: Holder) {
    holder && setHolder(holder)
  }

  function sectorsHandler(sectors: Sector[]) {
    sectors && setSectors(sectors)
  }

  function usersHandler(holderUsers: User[]) {
    users && setUsers(holderUsers)
  }

  return (
    <AppContext.Provider
      value={{
        user,
        users,
        token,
        holder,
        sectors,
        userHandler,
        usersHandler,
        tokenHandler,
        holderHandler,
        sectorsHandler,
      }}
    >
      {children}
    </AppContext.Provider>
  )
}

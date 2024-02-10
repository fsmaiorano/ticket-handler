import { Holder } from '@/models/holder'
import { User } from '@/models/user'
import { createContext, useState } from 'react'

interface IAppContextProps {
  children: React.ReactNode
}

interface IAppContext {
  user: User
  holder: Holder
  token: string
  userHandler: (user: User) => void
  tokenHandler: (token: string) => void
  holderHandler: (holder: Holder) => void
}

export const AppContext = createContext({} as IAppContext)

export function AppContextProvider({ children }: IAppContextProps) {
  const [user, setUser] = useState<User>({} as User)
  const [token, setToken] = useState<string>({} as string)
  const [holder, setHolder] = useState<Holder>({} as Holder)

  function userHandler(user: User) {
    user && setUser(user)
  }

  function tokenHandler(token: string) {
    token && setToken(token)
  }

  function holderHandler(holder: Holder) {
    holder && setHolder(holder)
  }

  return (
    <AppContext.Provider
      value={{ user, token, holder, userHandler, tokenHandler, holderHandler }}
    >
      {children}
    </AppContext.Provider>
  )
}

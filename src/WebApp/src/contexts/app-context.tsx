import { User } from '@/models/user'
import { createContext, useState } from 'react'

interface IAppContextProps {
  children: React.ReactNode
}

interface IAppContext {
  user: User
  token: string
  userHandler: (user: User) => void
  tokenHandler: (token: string) => void
}

export const AppContext = createContext({} as IAppContext)

export function AppContextProvider({ children }: IAppContextProps) {
  const [user, setUser] = useState<User>({} as User)
  const [token, setToken] = useState<string>({} as string)

  function userHandler(user: User) {
    console.log('userHandler', user)
    user && setUser(user)
    console.log('userHandler', user)
  }

  function tokenHandler(token: string) {
    token && setToken(token)
  }

  return (
    <AppContext.Provider value={{ user, token, userHandler, tokenHandler }}>
      {children}
    </AppContext.Provider>
  )
}

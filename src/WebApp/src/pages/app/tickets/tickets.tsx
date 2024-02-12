import { Button } from '@/components/ui/button'
import { Dialog, DialogContent, DialogTrigger } from '@/components/ui/dialog'
import { AppContext } from '@/contexts/app-context'
import { getSectors } from '@/services/get-sectors'
import { useContext, useEffect } from 'react'
import { Helmet } from 'react-helmet-async'
import { CreateTicket } from './create-ticket'

export function Tickets() {
  const { user, holder, sectors, sectorsHandler } = useContext(AppContext)

  const holderId = user.holderId

  // const { data: result } = useQuery({
  //   queryKey: ['sectors'],
  //   queryFn: () => getSectors({ holderId: user.id }),
  //   staleTime: Infinity,
  // })

  useEffect(() => {
    const fetchSectors = async () => {
      const getSectorsResponse = await getSectors({ holderId: holderId })
      sectorsHandler(getSectorsResponse)
    }

    fetchSectors()
  }, [holder, sectorsHandler, holderId])

  return (
    <>
      <Helmet title="Tickets" />
      <div className="flex flex-col gap-4">
        <h1 className="text-3xl font-bold tracking-tight">Tickets</h1>
        <p>{user.name}</p>

        <Dialog>
          <DialogTrigger asChild>
            <Button className="absolute right-6 top-20" variant={'outline'}>
              New Ticket
            </Button>
          </DialogTrigger>
          <DialogContent>
            <CreateTicket />
          </DialogContent>
        </Dialog>

        {holder &&
          sectors?.map((sector) => (
            <div key={sector.id} className="flex items-center justify-between">
              <div>
                <h2 className="text-xl font-bold">{sector.name}</h2>
                <p>{sector.id}</p>
              </div>
              <Button variant={'outline'}>View</Button>
            </div>
          ))}
      </div>
    </>
  )
}

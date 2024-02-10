import { Button } from '@/components/ui/button'
import { Dialog, DialogContent, DialogTrigger } from '@/components/ui/dialog'
import { AppContext } from '@/contexts/app-context'
import { getSectors } from '@/services/get-sectors'
import { useContext, useEffect } from 'react'
import { Helmet } from 'react-helmet-async'
import { CreateTicket } from './create-ticket'

export function Tickets() {
  const { user } = useContext(AppContext)

  const holderId = user.holderId

  // const { data: result } = useQuery({
  //   queryKey: ['sectors'],
  //   queryFn: () => getSectors({ holderId: user.id }),
  //   staleTime: Infinity,
  // })

  useEffect(() => {
    const fetchSectors = async () => {
      const sectors = await getSectors({ holderId: holderId })
      console.log(sectors)
    }

    fetchSectors()
  }, [holderId])

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
      </div>
    </>
  )
}

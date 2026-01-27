import { auth0 } from "@/lib/auth0";
import Link from "next/link";
import getLatestSeason from "./getLatestSeason";
import getAllowPicks from "./getAllowPicks";

export default async function Home() {
  const session = await auth0.getSession();

  const allowPicks = getAllowPicks(getLatestSeason());

  return (
    <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
      {allowPicks && 
      (session
        ? <Link href="/picks" className="text-2xl underline">Make your picks</Link>
        : <div>Login to make your picks</div>
      )}
    </div>
  );
}

puzzlePath = "puzzleinput.txt"

{- Gets the score of the stream. Outer groups
   have a base score of 1, each group inside another groups
   has a base score of their containing group + 1.
-}
getScore::String -> Int -> Int
getScore [] _ = 0
getScore (x:xs) score
    | x == '{'      = (getScore xs (score + 1))
    | x == '}'      = score + getScore xs (score - 1)
    | x == ','      = getScore xs score
    | x == '<'      = getScore (skipGarbage xs) score
    | otherwise     = getScore xs score

{- Discards elements until the end of the current garbage,
   and returns the remaining part of the string.
-}
skipGarbage::String -> String
skipGarbage (x:xs)
    | x == '>'      = xs
    | x == '!'      = skipGarbage (tail xs)
    | otherwise     = skipGarbage xs

{- Counts the amount of non-escaped garbage in the stream
   in number of characters. -}
getGarbage::String -> Bool -> Int -> Int
getGarbage [] _ chars = chars
getGarbage (x:xs) isGarbage chars
    | x == '<' && (not isGarbage)   = getGarbage xs True chars
    | x == '>' && isGarbage         = getGarbage xs False chars
    | x == '!'                      = getGarbage (tail xs) isGarbage chars
    | isGarbage                     = getGarbage xs isGarbage (chars + 1)
    | otherwise                     = getGarbage xs isGarbage chars

-- Reads a file into a single string.
getStream::FilePath -> IO String
getStream path = readFile path

main = do
    putStrLn "Stream Processing puzzle"
    stream <- getStream puzzlePath
    putStr "First task: "
    putStrLn (show(getScore stream 0))
    putStr "Second task: "
    putStrLn (show(getGarbage stream False 0))
package day_15;

public class Judge {
    /** Given two generators, returns the number of generated pairs where the last
        16 bits of their binary representation are the same.
        @param genA The first generator.
        @param genB The second generator.
        @param pairsToGenerate The number of pairs to generate and compare.
        @return The number of pairs generated that have the same last 16 bits.
     */
    public static int countPairs(Generator genA, Generator genB, int pairsToGenerate) {
        long valA;
        long valB;
        int pairCount = 0;

        for (int i = 0; i < pairsToGenerate; i++) {
            valA = genA.getNextValue();
            valB = genB.getNextValue();
            pairCount += judge(valA,valB) ? 1 : 0;
        }

        return pairCount;
    }

    /** Compares the last 16 bits of two 64 bit integers.
        @param valA The first integer.
        @param valB The second integer.
        @return Boolean indicating if the two integers have the same last 16 bits.
     */
    private static boolean judge(long valA, long valB) {
        long bitA = valA & 65535;
        long bitB = valB & 65535;

        return bitA == bitB;
    }
}

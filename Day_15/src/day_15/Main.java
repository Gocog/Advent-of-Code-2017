package day_15;

public class Main {
    public static void main(String[] args) {
        System.out.println("Dueling Generators puzzle");

        int genAInput = 883;
        int genBInput = 879;

        Generator genA = new Generator(genAInput,16807);
        Generator genB = new Generator(genBInput,48271);
        int pairCount = Judge.countPairs(genA,genB,40000000);
        System.out.println("First task: " + pairCount);

        genA = new Generator(genAInput,16807,4);
        genB = new Generator(genBInput,48271,8);
        pairCount = Judge.countPairs(genA,genB,5000000);
        System.out.println("Second task: " + pairCount);
    }
}
